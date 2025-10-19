import './App.css';
import { Outlet } from 'react-router-dom';
import Header from './components/Header';
import Footer from './components/Footer';
import Context from './context';
import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setUserDetails } from './store/userSlice';
import { Elements } from '@stripe/react-stripe-js';
import { loadStripe } from '@stripe/stripe-js';
import {jwtDecode} from 'jwt-decode';

const stripePromise = loadStripe('pk_test_51LmepVKiG1tYFgD1PO9vuaekHqSRcSWYyrTCL0fqsVysHsPXRvuZHAIOzkm7HjnMMU4zz1e6YkNNEqCRgCs5Odjn00udg25zRX');

function App() {
  const [cartCount, setCartCount] = useState(0);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const dispatch = useDispatch();

  const fetchUser = async (token) => {
    try {
      const decodedToken = jwtDecode(token);
      const currentTime = Date.now() / 1000; // Get current time in seconds

      // Check if token is expired
      if (decodedToken.exp < currentTime) {
        // Token is expired, clear it from local storage and reset state
        localStorage.removeItem('token');
        setIsAuthenticated(false);
        return;
      }

      const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      const userUsername = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
      console.log("user:", userUsername);

      const userResponse = await fetch(userRole === "Admin"
        ? `https://localhost:7123/admins/username/${userUsername}`
        : `https://localhost:7123/users/username/${userUsername}`, {
          method: 'GET',
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
        });

      if (!userResponse.ok) {
        throw new Error("Failed to fetch user data");
      }

      const userData = await userResponse.json();
      console.log("User data: ", userData);
      dispatch(setUserDetails({ token, user: userData, userRole }));
      setIsAuthenticated(true); // Set authenticated status

    } catch (error) {
      console.error("Error fetching user data:", error);
      localStorage.removeItem('token'); // Clear token on error
      setIsAuthenticated(false); // Reset authentication status
    }
  };

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      fetchUser(token); // Fetch user details if token exists
    }

    const storedCart = JSON.parse(localStorage.getItem('cart')) || [];
    setCartCount(storedCart.length);
  }, [dispatch]);

  return (
    <div className="flex flex-col min-h-screen">
      <Context.Provider value={{
        cartCount,
        setCartCount,
        isAuthenticated,
        setIsAuthenticated,
      }}>
        <Elements stripe={stripePromise}>
          <Header />
          <main className="flex-grow pt-16">
            <Outlet />
          </main>
          <Footer />
        </Elements>
      </Context.Provider>
    </div>
  );
}

export default App;
