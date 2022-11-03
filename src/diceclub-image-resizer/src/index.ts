import fs from 'fs';
import path from 'path';

import pMap from 'p-map';
import { Card } from 'scryfall-sdk';
import sharp from 'sharp';
import { processCard } from './card_processor';
import {
  processFileWithGoogleVision,
  processFileWithTesseract,
} from './file_processors';

const cardCache = new Map<string, Card>();

const baseDirectoryPath = path.join(
  `C:\\Users\\${process.env.USERNAME}\\OneDrive`,
  'mtg_to_scan',
);
const resizedDirectoryPath = path.join(baseDirectoryPath, 'resized');
const doneDiretoryPath = path.join(baseDirectoryPath, 'done');

const useGoogleVision = true;
const maxFilesCount = 20;
const maxParallerCount = 5;
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
      fs.renameSync(filePath, path.join(doneDiretoryPath, filename));
    }
  }
};


if (!fs.existsSync(doneDiretoryPath)) {
  fs.mkdirSync(doneDiretoryPath);
}

console.log('baseDirectoryPath', baseDirectoryPath);
console.log('resizedDirectoryPath', resizedDirectoryPath);
console.log('doneDirectoryPath', doneDiretoryPath);


fs.readdirSync(baseDirectoryPath).forEach((file) => files.push(file));

console.log('files', files.length);
files = files.slice(0, maxFilesCount);

void (async () => {
  await pMap(files, processFile, { concurrency: maxParallerCount });
  console.log('Done!');
  process.exit(0);
})();
