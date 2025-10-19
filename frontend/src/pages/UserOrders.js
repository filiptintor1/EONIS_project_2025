import React, { useContext, useEffect, useState } from 'react';
import displayCurrencyRSD from '../helpers/displayCurrency';
import { useSelector } from 'react-redux';
import moment from 'moment';
import Context from '../context';

const UserOrders = () => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  
  const user = useSelector((state) => state?.user?.user);
  const context = useContext(Context);
  console.log('USERRR: ', user)

  // Fetch orders for the current user
  const fetchUserOrders = async () => {
    console.log('USERRR for fetch: ', user)
    console.log('USERRR ID: ', user?.user?.userId)

    try {
      const response = await fetch(`https://localhost:7123/users/${user?.user?.userId}/orders`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${user?.token}`,
        },
      });

      const orderData = await response.json();

      console.log('Order Data: ', orderData );

      // Fetch order items for each order
      const enrichedOrders = await Promise.all(orderData.map(async (order) => {
        // Fetch order items for each order
        console.log('Order Data: ', orderData );
        const orderItemsResponse = await fetch(`https://localhost:7123/order-items/by-order/${order.orderId}`);
        const orderItemsDataRaw = await orderItemsResponse.json(); // prvo await
        const orderItemsData = Array.isArray(orderItemsDataRaw) ? orderItemsDataRaw : [];

        // Fetch product details for each item
        const enrichedItems = await Promise.all(orderItemsData.map(async (item) => {
          const productResponse = await fetch(`https://localhost:7123/products/${item.productId}`);
          const productData = await productResponse.json();
          console.log('Product Data: ', productData );
          return { ...item, productName: productData.name, productPrice: productData.price }; // Add product name and price
        }));

        return { ...order, items: enrichedItems };
      }));

      setOrders(enrichedOrders);
      setLoading(false);

    } catch (error) {
      console.error("Error occurred while fetching user orders:", error);
    }
  };

  useEffect(() => {
    fetchUserOrders();
  }, [user]);

  // Calculate total amount for an order
  const calculateTotalAmount = (items) => {
    return items.reduce((total, item) => {
      return total + item.productPrice * item.quantity;
    }, 0);
  };

  if (loading) return <p>Loading user orders...</p>;

  return (
    <div className="h-[calc(100vh-190px)] overflow-y-scroll">
      {!orders.length && <p>No orders available</p>}
      
      <div className="container mx-auto p-4 w-full">
        <h1 className='text-4xl mb-10 font-semibold w-fit border-b-2 py-3 border-orange-400 '>My Orders:</h1>
        {orders.map((order, i) => (
          <div key={order?.orderId + i} className="mb-6">
            <p className="font-medium text-lg">{moment(order.date).format('LL')}</p>
            <div className="border rounded p-4 relative">
              <div className="flex flex-col lg:flex-row justify-between">
                <div className="grid gap-1">
                  <p><strong>Order ID:</strong> {order.orderId}</p>
                  <p><strong>Is Paid:</strong> {order.isPaid ? <span className='text-green-500 font-semibold'>Yes</span> : <span className='text-red-500 font-semibold'>No</span>}</p>
                  <p><strong>Total Items:</strong> {order.items.length}</p>
                  <div>
                    <p className="font-bold">Order Items:</p>
                    {order.items.map((item) => (
                      <div key={item.productId} className="ml-4">
                        <p>Product Name: {item.productName}</p>
                        <p>Quantity: {item.quantity}</p>
                        <p>Price: {displayCurrencyRSD(item.productPrice)}</p>
                      </div>
                    ))}
                  </div>
                </div>
                <div className="text-right lg:ml-8">
                  <p className="font-bold text-xl">Total Amount:</p>
                  <p className="text-red-600 font-medium text-lg">
                    {displayCurrencyRSD(calculateTotalAmount(order.items))}
                  </p>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default UserOrders;
