import type { Language } from "../types/currency";
import { LanguageSelector } from "./LanguageSelector";

interface CurrencyFormProps {
  dollars: string;
  cents: string;
  language: Language;
  loading: boolean;
  onDollarsChange: (value: string) => void;
  onCentsChange: (value: string) => void;
  onLanguageChange: (language: Language) => void;
  onSubmit: () => void;
}

export function CurrencyForm({
  dollars,
  cents,
  language,
  loading,
  onDollarsChange,
  onCentsChange,
  onLanguageChange,
  onSubmit,
}: CurrencyFormProps) {
  return (
    <section className="card form-card">
      <div className="card-header">
        <div>
          <span className="eyebrow">INPUT</span>
          <h2>Enter an amount</h2>
        </div>

        <span className="currency-symbol">$</span>
      </div>

      <div className="amount-fields">
        <div className="field dollars-field">
          <label htmlFor="dollars">Dollars</label>

          <input
            id="dollars"
            type="text"
            inputMode="numeric"
            pattern="[0-9]*"
            value={dollars}
            onChange={(event) => {
              const value = event.target.value;

              if (/^\d{0,9}$/.test(value)) {
                onDollarsChange(value);
              }
            }}
            placeholder="999999999"
          />
        </div>

        <span className="decimal-separator">,</span>

        <div className="field cents-field">
          <label htmlFor="cents">Cents</label>

          <input
            id="cents"
            type="text"
            inputMode="numeric"
            pattern="[0-9]*"
            value={cents}
            onChange={(event) => {
              const value = event.target.value;

              if (/^\d{0,2}$/.test(value)) {
                onCentsChange(value);
              }
            }}
            placeholder="99"
          />
        </div>
      </div>

      <LanguageSelector value={language} onChange={onLanguageChange} />

      <button
        type="button"
        className="convert-button"
        onClick={onSubmit}
        disabled={loading}
      >
        {loading ? "Converting..." : "Convert to words"}
        {!loading && <span>→</span>}
      </button>
    </section>
  );
}
