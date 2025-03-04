import { useEffect, useState } from "react";
import { useSearchParams } from "react-router";
import { useLocation } from "react-router";

interface OrderDetails {
  total: number;
  customerEmail: string;
}

const SuccessPage = () => {
  const [searchParams] = useSearchParams();
  const sessionId = searchParams.get("sessionId");
  const [orderDetails, setOrderDetails] = useState<OrderDetails | null>(null);
  const [loading, setLoading] = useState(true);
  const location = useLocation();

  useEffect(() => {
    if (sessionId) {
      // Fetch order details from your backend
      fetch(
        `${
          import.meta.env.VITE_API_URL
        }checkout/checkout-success?sessionId=${sessionId}`
      )
        .then((response) => response.json())
        .then((data) => {
          setOrderDetails(data);
          setLoading(false);
        })
        .catch((error) => {
          console.error("Error fetching order details:", error);
          setLoading(false);
        });
      window.history.replaceState({}, document.title, location.pathname);
    }
  }, [sessionId]);

  if (loading) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h1>Payment Successful!</h1>
      {orderDetails ? (
        <div>
          <p>Session ID: {sessionId}</p>
          <p>Total: {(orderDetails?.total / 100).toFixed(2)}</p>
          <p>Customer Email: {orderDetails?.customerEmail}</p>
        </div>
      ) : (
        <p>Order details could not be fetched.</p>
      )}
    </div>
  );
};

export default SuccessPage;
