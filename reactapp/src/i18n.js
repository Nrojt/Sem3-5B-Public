// translation configuration
import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import { initReactI18next } from "react-i18next";

// Import all JSON files in the translations directory
const modules = import.meta.globEager("./translations/**/*.json");

// initializing the translations object, to be filled in the loop below
let translations = {};

// Loop over the keys (paths) in the modules object
for (const path in modules) {
  // Extract the language and namespace from the path
  const match = path.match(/\.\/translations\/(\w+)\/(\w+)\.json$/);
  if (match) {
    const lang = match[1];
    const namespace = match[2];
    // if there is no object for the language yet, create it
    if (!translations[lang]) {
      translations[lang] = {};
    }
    // Get the default export of the module and add it to the translations
    // object
    translations[lang][namespace] = modules[path].default;

    // console.log("Loaded translation", lang, namespace,
    // modules[path].default);
  }
}

// logging the translations object to the console
console.log(translations);

// setting up i18next with react-i18next and language detection
i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    resources: translations,
    detection: {
      // specify order and from where user language should be detected, navigator
      // means the browsers language
      order: ["localStorage", "navigator"],
      // saving the language in the browser's localstorage
      caches: ["localStorage"],
      // specify the key to lookup the language preference
      lookupLocalStorage: "i18nextLng",
    },
    fallbackLng: "nl", // use nl if a translation isn't available in the selected language
    interpolation: {
      escapeValue: false, // React already does escaping (escaping is turning
      // characters into representations, e.g. & -> &amp;)
    },
  });

export default i18n;
