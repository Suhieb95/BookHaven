/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_API_URL: string;
  readonly VITE_NODE_ENV: string;
  readonly VITE_CLIENT_URL: string;
  readonly VITE_API_EXCLUDED_FROM_ERRORS_URL: string;
  readonly VITE_FRONTEND_URL: string;
  readonly VITE_FILE_URL: string;
  readonly VITE_COMPANY_FILE_URL: string;
  readonly VITE_USERS_URL: string;
  readonly VITE_TURNSTILE_SITEKEY: string;
  readonly VITE_GOOGLE_CLIENTID: string;
  readonly VITE_UTM_TRACKER: string;
  readonly VITE_STRIPE_KEY: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}
