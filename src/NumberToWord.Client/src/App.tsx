import { useState } from "react";

import "./App.css";

import { CurrencyForm } from "./components/CurrencyForm";
import { ConversionResult } from "./components/ConversionResult";
import type { Language } from "./types/currency";
import { convertCurrency } from "./service/currencyApi";

function App() {
  const [dollars, setDollars] = useState("");
  const [cents, setCents] = useState("");
  const [language, setLanguage] = useState<Language>("en");

  const [result, setResult] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  async function handleConvert() {
    setError(null);
    setResult(null);

    const dollarsValue = Number(dollars);
    const centsValue = Number(cents);

    if (!Number.isInteger(dollarsValue) || dollarsValue < 0) {
      setError("Please enter a valid dollar amount.");
      return;
    }

    if (
      !Number.isInteger(centsValue) ||
      centsValue < 0 ||
      centsValue > 99
    ) {
      setError("Cents must be a whole number between 0 and 99.");
      return;
    }

    if (dollarsValue > 999999999) {
      setError("The maximum amount is 999,999,999 dollars.");
      return;
    }

    try {
      setLoading(true);

      const response = await convertCurrency({
        dollars: dollarsValue,
        cents: centsValue,
        language,
      });

      setResult(response.text);
    } catch (err) {
      setError(
        err instanceof Error
          ? err.message
          : "Something went wrong while converting the amount.",
      );
    } finally {
      setLoading(false);
    }
  }

  return (
    <main className="app">
      <div className="background-decoration" />

      <div className="container">
        <header className="hero">
          <div className="logo-mark">123</div>

          <span className="eyebrow">CURRENCY · WORDS</span>

          <h1>
            Numbers,
            <br />
            <span>expressed.</span>
          </h1>

          <p>
            Convert dollar amounts into words with support for
            multiple languages.
          </p>
        </header>

        <div className="converter">
          <CurrencyForm
            dollars={dollars}
            cents={cents}
            language={language}
            loading={loading}
            onDollarsChange={setDollars}
            onCentsChange={setCents}
            onLanguageChange={setLanguage}
            onSubmit={handleConvert}
          />

          <div className="conversion-arrow">
            <span>↓</span>
          </div>

          <ConversionResult
            result={result}
            error={error}
          />
        </div>

        <footer>
          <span>NumberToWord</span>
          <span>Server-side conversion</span>
        </footer>
      </div>
    </main>
  );
}

export default App;