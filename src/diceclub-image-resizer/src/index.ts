import fs from 'fs';
import path from 'path';

import chokidar from 'chokidar';
import { program } from 'commander';
import pMap from 'p-map';
import { Card } from 'scryfall-sdk';
import sharp from 'sharp';
import { ILoginResult, loginToWebService, sendToWebService } from './action';
import { processCard } from './card_processor';
import {
  processFileWithGoogleVision,
  processFileWithTesseract,
} from './file_processors';


const cardCache = new Map<string, Card>();
const baseUrl = 'http://localhost:5280';


let authResult: ILoginResult;

program
  .name('diceclub-image-processor')
  .description('Process images from a directory')
  .option('--use-google-vision', undefined, true)
  .option('--bulk-scan', undefined, true)
  .option('--max-files-count <number>')
  .option('--max-parallel-count <number>');

program.parse(process.argv);
const options = program.opts();

const bulkScan = options.bulkScan;
const useGoogleVision = options.useGoogleVision;
const maxFilesCount = options.maxFilesCount;
const maxParallerCount = options.maxParallelCount || 1;


const baseDirectoryPath = path.join(
  `C:\\Users\\${process.env.USERNAME}\\OneDrive`,
  'mtg_to_scan',
);
const resizedDirectoryPath = path.join(baseDirectoryPath, 'resized');
const doneDiretoryPath = path.join(baseDirectoryPath, 'done');
const badDirectoryPath = path.join(baseDirectoryPath, 'bad');


let files: string[] = [];

const processFile = async (filename: string, index: number): Promise<void> => {
  if (filename.endsWith('.jpg')) {
    const filePath = path.join(baseDirectoryPath, filename);
    const resizedFilePath = path.join(resizedDirectoryPath, `${index}.jpg`);
    // console.log(
    //   `[${index}/${files.length}]] Resizing ${filePath} to ${resizedFilePath}`,
    // );

    const image = sharp(filePath);
    await image
      .resize(370, 546)
      .extract({ left: 15, top: 10, width: 295, height: 50 })
      .greyscale()
    //.linear(contrast, (128 * contrast) + 128)
    //.modulate({ brightness: brightness })
      .toFile(resizedFilePath);

    let resultText = '';
    if (useGoogleVision) {
      resultText = await processFileWithGoogleVision(resizedFilePath);
    } else {

      resultText = await processFileWithTesseract(resizedFilePath);
    }
    let card: Card | null = new Card();
    let foundInCache = false;
    if (cardCache.has(resultText)) {
      card = cardCache.get(resultText)!;
      foundInCache= true;
    } else {
      card = await processCard(resultText)!;
    }

    if (card) {
      console.log(
        `[${index}/${files.length}] - Found card in cache: ${foundInCache}: ${card.id} - ${card.name} -- [${card.set_name}] - ${card.lang} - (${card.printed_name})`,
      );
      cardCache.set(resultText, card);
      await sendToWebService(baseUrl, card, (await authResult).accessToken);
      fs.renameSync(filePath, path.join(doneDiretoryPath, filename));

    } else {
      console.log(`[${index}/${files.length}] - Card not found: ${resultText}`);
      fs.renameSync(filePath, path.join(badDirectoryPath, filename));
    }
  }
};


if (!fs.existsSync(doneDiretoryPath)) {
  fs.mkdirSync(doneDiretoryPath);
}

if (!fs.existsSync(badDirectoryPath)) {
  fs.mkdirSync(badDirectoryPath);
}

console.log('baseDirectoryPath', baseDirectoryPath);
console.log('resizedDirectoryPath', resizedDirectoryPath);
console.log('doneDirectoryPath', doneDiretoryPath);


fs.readdirSync(baseDirectoryPath).forEach((file) => files.push(file));

console.log('files', files.length);
if (maxFilesCount !== -1) {
  files = files.slice(0, maxFilesCount);
}
void (async () => {
  authResult = await loginToWebService(baseUrl, 'squid@stormwind.it', 'xTFfJ7doKe');
  if (authResult.accessToken) {
    console.log('Authentication done!');
  }

  if (bulkScan) {
    await pMap(files, processFile, { concurrency: maxParallerCount });
    console.log('Done!');
    process.exit(0);
  } else {
    console.log('Ready to scan!');
    chokidar
      .watch(baseDirectoryPath, {
        ignoreInitial: true,
        awaitWriteFinish: {
          stabilityThreshold: 2000,
          pollInterval: 100,
        },
      })
      .on('add', async (filePath) => {
        console.log('File', filePath, 'has been added');
        const filename = path.basename(filePath);
        if (filename.endsWith('.jpg')) {
          await new Promise((resolve) => setTimeout(resolve, 1000));
          await processFile(filename, 1);
        }
      });
  }
})();
