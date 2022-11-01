import fs from 'fs';
import path from 'path';

import tesseract from 'node-tesseract-ocr';

import pMap from 'p-map';
import sharp from 'sharp';
let files: string[] = [];
//const contrast = 1.5;
//const brightness = 0.5;
const resizeFile = async (filename: string, index: number): Promise<void> => {
  if (filename.endsWith('.jpg')) {
    const filePath = path.join(baseDirectoryPath, filename);
    const resizedFilePath = path.join(resizedDirectoryPath, `${index}.jpg`);
    console.log(
      `[${index}/${files.length}]] Resizing ${filePath} to ${resizedFilePath}`,
    );

    const image = sharp(filePath);
    await image
      .resize(370, 546)
      .extract({ left: 15, top: 10, width: 295, height: 50 })
      .greyscale()
    //.linear(contrast, (128 * contrast) + 128)
    //.modulate({ brightness: brightness })
      .toFile(resizedFilePath);

    const test = await tesseract.recognize(resizedFilePath, {
      lang: 'ita',
      oem: 1,
      psm: 3,
    });
    console.log(`[${index}/${files.length}]] ${test}`);
  }
};

const baseDirectoryPath = path.join(
  'C:\\Users\\squid\\OneDrive',
  'mtg_to_scan',
);
const resizedDirectoryPath = path.join(baseDirectoryPath, 'resized');

console.log('baseDirectoryPath', baseDirectoryPath);
console.log('resizedDirectoryPath', resizedDirectoryPath);

fs.readdirSync(baseDirectoryPath).forEach((file) => files.push(file));

console.log('files', files.length);
files = files.slice(0, 300);

void (async () => {
  await pMap(files, resizeFile, { concurrency: 10 });
})();
