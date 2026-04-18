import { useEffect, useState } from "react";
import { defaultBusinessSettings } from "@/config/business-info";
import { businessSettingsService } from "@/services/business-settings";
import type { BusinessSettings } from "@/types";

export function useBusinessSettings() {
  const [settings, setSettings] = useState<BusinessSettings>(defaultBusinessSettings);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    let isMounted = true;

    const loadSettings = async () => {
      try {
        const data = await businessSettingsService.getPublic();
        if (isMounted) {
          setSettings(data);
        }
      } catch {
        if (isMounted) {
          setSettings(defaultBusinessSettings);
        }
      } finally {
        if (isMounted) {
          setIsLoading(false);
        }
      }
    };

    loadSettings();

    return () => {
      isMounted = false;
    };
  }, []);

  return {
    settings,
    setSettings,
    isLoading,
  };
}
