interface ConversionResultProps {
  result: string | null;
  error: string | null;
}

export function ConversionResult({
  result,
  error,
}: ConversionResultProps) {
  return (
    <section className="card result-card">
      <span className="eyebrow">RESULT</span>

      {error ? (
        <div className="error-message">
          <span>!</span>
          <p>{error}</p>
        </div>
      ) : result ? (
        <>
          <p className="result-label">Your amount in words</p>

          <p className="result-text">{result}</p>
        </>
      ) : (
        <div className="empty-result">
          <div className="empty-icon">Aa</div>
          <p>Your converted amount will appear here.</p>
        </div>
      )}
    </section>
  );
}