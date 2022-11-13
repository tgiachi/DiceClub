import { readFileSync } from 'fs';
import vision from '@google-cloud/vision';
import tesseract from 'node-tesseract-ocr';

const client = new vision.ImageAnnotatorClient({
  keyFilename: `C:\\Users\\${process.env.USERNAME}\\Downloads\\translate-nmefqw-3dd83d24b91d.json`,
});

const processFileWithTesseract = async (
  file: string,
  lang: string = 'ita',
): Promise<string> => {
  const resultText = await tesseract.recognize(file, {
    lang,
    oem: 1,
    psm: 3,
  });

  return resultText;
};

const processFileWithGoogleVision = async (file: string): Promise<string> => {


  const [textDetection] = await client.textDetection(readFileSync(file));

  return textDetection.fullTextAnnotation?.text ?? '';
};

export { processFileWithGoogleVision, processFileWithTesseract };
