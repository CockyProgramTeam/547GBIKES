import React, { useState } from "react";
import { useLocation } from "react-router-dom";

export default function Details2() {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const parkId = queryParams.get("parkid"); // still displayed

  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const fetchReviews = async () => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch("https://parksapi.547bikes.info/api/ParkReview", {
        headers: {
          Accept: "application/json",
        },
      });

      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }

      const data = await response.json();
      setReviews(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1>SomeDetailsPage via URL</h1>
      <p>Park ID (from URL): {parkId}</p>

      <button onClick={fetchReviews}>Load All Reviews</button>

      {loading && <p>Loading reviews...</p>}
      {error && <p style={{ color: "red" }}>Error: {error}</p>}

      <ul>
        {reviews.map((review) => (
          <li key={review.id}>
            <strong>{review.stars}⭐</strong> - {review.description} <br />
            <em>Park ID: {review.parkId} | Posted: {review.datePosted}</em>
          </li>
        ))}
      </ul>
    </div>
  );
}

