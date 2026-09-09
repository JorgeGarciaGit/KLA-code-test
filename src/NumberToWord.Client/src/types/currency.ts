export type Language = "en" | "de";

export interface ConvertCurrencyRequest {
  dollars: number;
  cents: number;
  language: Language;
}

export interface ConvertCurrencyResponse {
  text: string;
}