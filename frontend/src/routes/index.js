import App from "../App";
import AboutUs from "../pages/AboutUs";
import AdminPanel from "../pages/AdminPanel";
import AllAdmins from "../pages/AllAdmins";
import AllOrders from "../pages/AllOrders";
import AllProducts from "../pages/AllProducts";
import AllUsers from "../pages/AllUsers";
import Cart from "../pages/Cart";
import CategoryProduct from "../pages/CategoryProduct";
import EditAdminProfile from "../pages/EditAdminProfile";
import EditProfile from "../pages/EditProfile";
import Fail from "../pages/Fail";
import Home from "../pages/Home";
import Login from "../pages/Login";
import ProductPage from "../pages/ProductPage";
import SignUp from "../pages/SignUp";
import Success from "../pages/Success";
import UserOrders from "../pages/UserOrders";

const { createBrowserRouter } = require("react-router-dom");

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            {
                path: "",
                element: <Home />
            },
            {
                path: "login",
                element: <Login />
            },
            {
                path: "sign-up",
                element: <SignUp />
            },
            {
                path: "category-product",
                element: <CategoryProduct />
            },
            {
                path: "cart",
                element: <Cart />
            },
            {
                path: "about-us",
                element: <AboutUs />
            },
            {
                path: "product-page/:id",
                element: <ProductPage />
            },
            {
                path: "edit-profile",
                element: <EditProfile />
            },
            {
                path: "user-orders",
                element: <UserOrders />
            },
            {
                path: "checkout/success",
                element: <Success />
            },
            {
                path: "checkout/fail",
                element: <Fail />
            },
            {
                path: "admin-panel",
                element: <AdminPanel />,
                children: [
                    {
                      path: "all-users",
                      element: <AllUsers />,
                    },
                    {
                        path: "all-admins",
                        element: <AllAdmins />,
                      },
                    {
                      path: "all-products",
                      element: <AllProducts />,
                    },
                    {
                      path: "all-orders",
                      element: <AllOrders />
                    },
                    {
                        path: "edit-admin-profile",
                        element: <EditAdminProfile />
                    }
                  ],
            },
            
        ]
    }
])

export default router