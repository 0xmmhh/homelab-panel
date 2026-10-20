import type { InitialHookStatus } from '@react-buddy/ide-toolbox'
import { useState } from 'react'

// Runs before React Buddy renders a preview. Nothing to initialize yet:
// add async setup here (set loading while it runs, error if it fails).
export const useInitial: () => InitialHookStatus = () => {
  const [status] = useState<InitialHookStatus>({
    loading: false,
    error: false,
  })
  return status
}
