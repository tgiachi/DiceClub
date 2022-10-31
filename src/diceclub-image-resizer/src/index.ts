import fs from 'fs';
import path from 'path';
import pMap from 'p-map';

import sharp from 'sharp';

const resizeFile = async (filename: string): Promise<void> => {
  if (filename.endsWith('.jpg')) {
    const filePath = path.join(baseDirectoryPath, filename);
    const resizedFilePath = path.join(resizedDirectoryPath, filename);
    console.log(`Resizing ${filePath} to ${resizedFilePath}`);

    const image = sharp(filePath);
    await image.resize(370, 546).toFile(resizedFilePath);
  }
};

const baseDirectoryPath = path.join(
  'C:\\Users\\squid\\OneDrive',
  'mtg_to_scan',
);
const resizedDirectoryPath = path.join(baseDirectoryPath, 'resized');

console.log('baseDirectoryPath', baseDirectoryPath);
console.log('resizedDirectoryPath', resizedDirectoryPath);

const files: string[] = [];
fs.readdirSync(baseDirectoryPath).forEach((file) => files.push(file));

console.log('files', files.length);

void (async () => {
  await pMap(files, resizeFile, { concurrency: 10 });
})();
