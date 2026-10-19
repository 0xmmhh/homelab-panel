import react from '@vitejs/plugin-react'
import { defineConfig, loadEnv } from 'vite'

// https://vite.dev/config/
export default defineConfig(({ mode }) => {
  // Not VITE_-prefixed: used only by the dev server, never shipped to the browser.
  const env = loadEnv(mode, process.cwd(), '')
  const apiTarget = env.API_PROXY_TARGET ?? 'http://localhost:5203'

  return {
    plugins: [react()],
    server: {
      // Same-origin calls in dev: no CORS, and the UI only ever talks to our API.
      proxy: {
        '/api': apiTarget,
      },
    },
  }
})
