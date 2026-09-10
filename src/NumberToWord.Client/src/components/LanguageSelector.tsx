import type { Language } from "../types/currency";

interface LanguageSelectorProps {
  value: Language;
  onChange: (language: Language) => void;
}

const languages: { value: Language; label: string }[] = [
  { value: "en", label: "English" },
  { value: "de", label: "Deutsch" },
];
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
        {languages.map(({ value, label }) => (
          <option key={value} value={value}>
            {label}
          </option>
        ))}
      </select>
    </div>
  );
}