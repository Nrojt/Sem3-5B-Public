import plugin from "@vitejs/plugin-react";
import fs from "fs";
import { fileURLToPath, URL } from "node:url";
import path from "path";
import { defineConfig } from "vite";

// Flag to determine if HTTPS should be disabled, to be set in the .env file
// (for github actions)
const disableHttps = process.env.DISABLE_HTTPS === "true";

let httpsOptions = {};
let certFilePath;
let keyFilePath;

console.log("disableHttps", disableHttps);

if (!disableHttps) {
  const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ""
      ? `${process.env.APPDATA}/ASP.NET/https`
      : `${process.env.HOME}/.aspnet/https`;
  const certificateArg = process.argv
    .map((arg) => arg.match(/--name=(?<value>.+)/i))
    .filter(Boolean)[0];
  const certificateName = certificateArg
    ? certificateArg.groups.value
    : "reactapp";

  if (!certificateName) {
    console.error(
      "Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.",
    );
    process.exit(-1);
  }

  certFilePath = path.join(baseFolder, `${certificateName}.pem`);
  keyFilePath = path.join(baseFolder, `${certificateName}.key`);

  httpsOptions = {
    key: fs.readFileSync(keyFilePath),
    cert: fs.readFileSync(certFilePath),
  };
}

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [plugin()],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
  server: {
    proxy: {
      "^/api/": {
        target: "https://localhost:7245/",
        secure: false, // for self-signed certificates
        changeOrigin: true, // needed for virtual hosted sites
        ws: true, // proxy websockets
      },
    },
    port: 5173,
    https: disableHttps ? undefined : httpsOptions,
  },
});
