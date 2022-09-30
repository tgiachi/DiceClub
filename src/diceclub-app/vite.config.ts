import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import { chunkSplitPlugin } from "vite-plugin-chunk-split";
import dynamicImport from "vite-plugin-dynamic-import";
import reactRefresh from "@vitejs/plugin-react-refresh";
import { Plugin as importToCDN } from "vite-plugin-cdn-import";
import { autoComplete } from "vite-plugin-cdn-import";

// https://vitejs.dev/config/
export default defineConfig({
	plugins: [
		react(),
		chunkSplitPlugin({ strategy: "single-vendor" }),
		dynamicImport(),
		importToCDN({
			modules: [autoComplete("react"), autoComplete("react-dom")],
		}),
	],
	build: {
		minify: true,
		chunkSizeWarningLimit: 1000,
		rollupOptions: {
			output: {
				manualChunks: {
					vendor: ["react", "react-router-dom", "react-dom"],
				},
			},
		},
	},
});
