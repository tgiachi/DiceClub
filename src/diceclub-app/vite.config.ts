import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import { chunkSplitPlugin } from "vite-plugin-chunk-split";
import dynamicImport from "vite-plugin-dynamic-import";
import reactRefresh from "@vitejs/plugin-react-refresh";
import { Plugin as importToCDN } from "vite-plugin-cdn-import";
import { autoComplete } from "vite-plugin-cdn-import";

import { dependencies } from './package.json';
function renderChunks(deps: Record<string, string>) {
  let chunks = {};
  Object.keys(deps).forEach((key) => {
    if (['react', 'react-router-dom', 'react-dom'].includes(key)) return;
    chunks[key] = [key];
  });
  return chunks;
}

// https://vitejs.dev/config/
export default defineConfig({
	plugins: [
		react(),
		chunkSplitPlugin(),
		dynamicImport(),
		importToCDN({
			modules: [autoComplete("react"), autoComplete("react-dom")],
		})
	],
	build: {
		rollupOptions: {
			output: {
				manualChunks: {
				 vendor: ['react', 'react-router-dom', 'react-dom'],
          ...renderChunks(dependencies),
				},
			},
		},
	},
});
