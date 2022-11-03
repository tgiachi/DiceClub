const { typescript } = require('projen');
const project = new typescript.TypeScriptAppProject({
  defaultReleaseBranch: 'alpha',
  name: 'diceclub-image-resizer',
  scripts: {
    debug: 'tsc --build && node lib/index.js',
  },

  deps: [
    'p-map@4.0.0',
    'sharp',
    'node-tesseract-ocr',
    'fs-extra',
    '@google-cloud/vision',
    'scryfall-sdk',
  ] /* Runtime dependencies of this module. */,
  // description: undefined,  /* The description is just a string that helps people understand the purpose of the package. */
  devDeps: [
    '@types/sharp',
    '@types/fs-extra',
  ] /* Build dependencies for this module. */,
  // packageName: undefined,  /* The "name" in package.json. */
});
project.synth();
