import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  user: null, // The user object will hold details like userId, username, role, etc.
};

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    // This action sets the user details in the Redux state when a user logs in
    setUserDetails: (state, action) => {
      state.user = action.payload; // The payload contains user data (e.g., userId, username, role)
    },

    // This action clears the user details, typically called when a user logs out
    clearUserDetails: (state) => {
      state.user = null; // Clear the user data from state
    },
  },
});

// Export the actions to be used in components
export const { setUserDetails, clearUserDetails } = userSlice.actions;

// Export the reducer to be added to the store
export default userSlice.reducer;
