import type {
  ConvertCurrencyRequest,
  ConvertCurrencyResponse,
} from "../types/currency";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

export async function convertCurrency(
  request: ConvertCurrencyRequest,
): Promise<ConvertCurrencyResponse> {
console.log("Request payload:", request);

  const response = await fetch(`${API_BASE_URL}/currency/convert`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(request),
  });

  if (!response.ok) {
    let message = "Unable to convert the currency.";

    try {
      const error = await response.json();

      if (typeof error?.message === "string") {
        message = error.message;
      }
    } catch {
      console.error("Failed to parse error response as JSON.");
    }

    throw new Error(message);
  }

  return response.json();
}