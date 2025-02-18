module.exports = {
  root: true,
  env: { browser: true, es2020: true },
  extends: ["eslint:recommended", "plugin:@typescript-eslint/recommended", "plugin:react-hooks/recommended"],
  ignorePatterns: ["dist", ".eslintrc.cjs"],
  parser: "@typescript-eslint/parser",
  plugins: ["react-refresh", "@typescript-eslint"], // Ensure all plugins are listed here
  rules: {
    "no-empty": ["error", { allowEmptyCatch: true }],
    "@typescript-eslint/no-explicit-any": "off",
    "react-refresh/only-export-components": ["warn", { allowConstantExport: true, allowExportNames: ["loader"] }],
  },
};
