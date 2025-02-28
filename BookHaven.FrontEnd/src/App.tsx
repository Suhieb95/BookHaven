import { BrowserRouter as Router, Routes, Route } from "react-router";
import CheckoutButton from "./CheckoutButton";
import SuccessPage from "./SuccessPage";
import CancelPage from "./CancelPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/">
          <Route path="" index element={<CheckoutButton />} />
          <Route path="checkout/success" element={<SuccessPage />} />
          <Route path="checkout/cancel" element={<CancelPage />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
