import type { Language } from "../types/currency";

interface LanguageSelectorProps {
  value: Language;
  onChange: (language: Language) => void;
}

const languages: Language[] = ["en", "de"];

export function LanguageSelector({
  value,
  onChange,
}: LanguageSelectorProps) {
  return (
    <div className="field">
      <label htmlFor="language">Language</label>

      <select
        id="language"
        value={value}
        onChange={(event) => onChange(event.target.value as Language)}
      >
        {languages.map((language) => (
          <option key={language} value={language}>
            {language}
          </option>
        ))}
      </select>
    </div>
  );
}