import { CartItem } from "../models/cartItem";

export default class CartService {
    private CART_KEY = 'rideFinderExampleApp';

    loadCart = (): CartItem[] => {
        const data = localStorage.getItem(this.CART_KEY);
        return data ? JSON.parse(data) : [];
    }

    addItemToCart = (newItem: CartItem) => {
        const cart = this.loadCart();
        const itemInCart = cart.findIndex((item: CartItem) => item.park.id === newItem.park.id);
        if (itemInCart > -1) {
            this.updateCart(cart[itemInCart], newItem);
        }
        cart.push(newItem);
        this.save(cart);
    }

    removeItemFromCart = (remItem: CartItem) => {
        const cart = this.loadCart();
        const result = cart.filter((val: CartItem) => val.park.id !== remItem.park.id);
        this.save(result);
    }

    updateCart(oldItem: CartItem, newItem: CartItem) {
        const cart = this.loadCart();
        const combinedItem = {
            park: newItem.park || oldItem.park,
            numDays: newItem.numDays || oldItem.numDays,
            numAdults: newItem.numAdults || oldItem.numAdults,
            numKids: newItem.numKids || oldItem.numKids
        };
        const index = cart.findIndex((val: CartItem) => val.park.id === combinedItem.park.id);
        if (index > -1) {
            cart[index] = combinedItem;
        }
        this.save(cart);
    }

    clearCart = () => {
        localStorage.removeItem(this.CART_KEY);
    }

    // 👉 New function: finalize booking with fixed header and POST to API
    finalizeBooking = async (): Promise<any> => {
        const cart = this.loadCart();

        const booking = {
            header: {
                uid: 10000,
                fullname: "capgemeni user",
                email: "capgemeniuser@547bikes.info",
                username: "capgemeniuser@547bikes.info",
                bookingId: Date.now(),
                bookingDate: new Date().toISOString(),
                totalItems: cart.length,
                totalCost: cart.reduce((sum, item) => {
                    const adultCost = item.numAdults * item.park.adultPrice;
                    const kidCost = item.numKids * item.park.childPrice;
                    return sum + (adultCost + kidCost) * item.numDays;
                }, 0)
            },
            items: cart.map(item => ({
                parkId: item.park.id,
                parkName: item.park.parkName,
                location: item.park.location,
                numDays: item.numDays,
                numAdults: item.numAdults,
                numKids: item.numKids,
                cost: (item.numAdults * item.park.adultPrice +
                       item.numKids * item.park.childPrice) * item.numDays
            }))
        };

        console.log("Finalized booking:", booking);

        try {
            const response = await fetch("https://parksapi.547bikes.info/api/CGCart", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(booking)
            });

            if (!response.ok) {
                throw new Error(`Failed to post booking: ${response.statusText}`);
            }

            const result = await response.json();
            console.log("Booking successfully posted:", result);

            // ✅ Clear cart only after successful post
            this.clearCart();

            return result;
        } catch (err) {
            console.error("Error posting booking:", err);
            // Optionally: return booking so caller can retry
            return booking;
        }
    }

    private save(cart: CartItem[]) {
        localStorage.setItem(this.CART_KEY, JSON.stringify(cart));
    }
}
