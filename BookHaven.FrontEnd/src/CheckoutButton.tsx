import { loadStripe } from "@stripe/stripe-js";

const stripePromise = loadStripe(import.meta.env.VITE_STRIPE_KEY);

const CheckoutButton = () => {
  const handleCheckout = async () => {
    try {
      const response = await fetch(
        import.meta.env.VITE_API_URL + "checkout/create-session",
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            title: "Sample Book",
            description: "A great book",
            price: 1999, // conver to Dhs From fills
            customerEmail: "test@gmail.com",
            imageUrl:
              "https://images.pexels.com/photos/674010/pexels-photo-674010.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
          }),
        }
      );

      const data = await response.json();
      console.log("API Response:", data); // Log API response

      if (!data.sessionId) {
        throw new Error("Session ID is missing in API response");
      }

      const stripe = await stripePromise;
      const { error } = await stripe!.redirectToCheckout({
        sessionId: data.sessionId,
      });

      if (error) {
        console.error("Stripe Checkout Error:", error);
      }
    } catch (error) {
      console.error("Error redirecting to checkout:", error);
    }
  };

  return <button onClick={handleCheckout}>Buy Now</button>;
};

export default CheckoutButton;
