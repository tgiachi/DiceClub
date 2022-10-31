const { typescript } = require('projen');
const project = new typescript.TypeScriptAppProject({
  defaultReleaseBranch: 'alpha',
  name: 'diceclub-image-resizer',

  deps: ['p-map@4.0.0', 'sharp'] /* Runtime dependencies of this module. */,
  // description: undefined,  /* The description is just a string that helps people understand the purpose of the package. */
  devDeps: ['@types/sharp'] /* Build dependencies for this module. */,
  // packageName: undefined,  /* The "name" in package.json. */
});
project.synth();
