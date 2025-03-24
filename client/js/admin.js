const API_BASE_URL = "https://localhost:7122/api";

document.addEventListener("DOMContentLoaded", loadAdminData);

async function loadAdminData() {
    await loadBooks();
    await loadOrders();
    await loadReviews();
}

/* Books Section */
async function loadBooks() {
    const bookList = document.getElementById("bookList");
    bookList.innerHTML = "";

    try {
        const response = await fetch(`${API_BASE_URL}/Books`);
        if (!response.ok) throw new Error("Failed to fetch books");

        const books = await response.json();
        books.forEach(book => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${book.id}</td>
                <td>${book.title}</td>
                <td>${book.shortDescription}</td>
                <td>${book.fullDescription}</td>
                <td><img src="${book.imageUrl}" alt="${book.title}" width="50"></td>
                <td>$${book.price.toFixed(2)}</td>
                <td>${book.purchaseCount}</td>
                <td>
                    <button class="edit" onclick="editBook(${book.id})">Edit</button>
                    <button class="delete" onclick="deleteBook(${book.id})">Delete</button>
                </td>
            `;
            bookList.appendChild(row);
        });
    } catch (error) {
        console.error("Error loading books:", error);
    }
}

/* Create & Update Book */
document.getElementById("bookForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const bookId = document.getElementById("bookId").value;
    const bookData = {
        title: document.getElementById("bookTitle").value,
        shortDescription: document.getElementById("bookShortDescription").value,
        fullDescription: document.getElementById("bookFullDescription").value,
        price: parseFloat(document.getElementById("bookPrice").value),
        imageUrl: document.getElementById("bookImageUrl").value,
        purchaseCount: parseInt(document.getElementById("bookPurchaseCount").value) || 0
    };

    try {
        if (bookId) {
            await fetch(`${API_BASE_URL}/Books/${bookId}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(bookData),
            });
            alert("Book updated successfully!");
        } else {
            await fetch(`${API_BASE_URL}/Books`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(bookData),
            });
            alert("Book added successfully!");
        }

        document.getElementById("bookForm").reset();
        document.getElementById("bookId").value = "";
        loadBooks();
    } catch (error) {
        console.error("Error saving book:", error);
    }
});

/* Edit Book */
async function editBook(bookId) {
    try {
        const response = await fetch(`${API_BASE_URL}/Books/${bookId}`);
        if (!response.ok) throw new Error("Failed to fetch book");

        const book = await response.json();
        document.getElementById("bookId").value = book.id;
        document.getElementById("bookTitle").value = book.title;
        document.getElementById("bookShortDescription").value = book.shortDescription;
        document.getElementById("bookFullDescription").value = book.fullDescription;
        document.getElementById("bookPrice").value = book.price;
        document.getElementById("bookImageUrl").value = book.imageUrl;
        document.getElementById("bookPurchaseCount").value = book.purchaseCount;
    } catch (error) {
        console.error("Error editing book:", error);
    }
}

/* Delete Book */
async function deleteBook(bookId) {
    if (!confirm("Are you sure you want to delete this book?")) return;

    try {
        await fetch(`${API_BASE_URL}/Books/${bookId}`, { method: "DELETE" });
        alert("Book deleted successfully!");
        loadBooks();
    } catch (error) {
        console.error("Error deleting book:", error);
    }
}

/* Orders Section */
async function loadOrders() {
    const orderList = document.getElementById("orderList");
    orderList.innerHTML = "";

    try {
        const response = await fetch(`${API_BASE_URL}/Orders`);
        if (!response.ok) throw new Error("Failed to fetch orders");

        const orders = await response.json();
        const books = await (await fetch(`${API_BASE_URL}/Books`)).json();
        orders.forEach(order => {
            const book = books.find(b => b.id === order.bookId);
            const bookTitle = book ? book.title : "Unknown";

            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${order.id}</td>
                <td>${bookTitle}</td>
                <td>${order.customerName}</td>
                <td>${order.quantity}</td>
                <td>${order.address}</td>
                <td>
                    <button class="edit" onclick="editOrder(${order.id})">Edit</button>
                    <button class="delete" onclick="deleteOrder(${order.id})">Delete</button>
                </td>
            `;
            orderList.appendChild(row);
        });
    } catch (error) {
        console.error("Error loading orders:", error);
    }
}

/* Create and Update Order */
document.getElementById("orderForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const orderId = document.getElementById("orderId").value;
    const bookId = parseInt(document.getElementById("orderBookId").value);
    const newQuantity = parseInt(document.getElementById("orderQuantity").value);
    const orderData = {
        bookId: bookId,
        customerName: document.getElementById("orderCustomerName").value,
        quantity: newQuantity,
        address: document.getElementById("orderAddress").value
    };

    try {
        const bookResponse = await fetch(`${API_BASE_URL}/Books/${bookId}`);
        if (!bookResponse.ok) throw new Error("Failed to fetch book details");

        const book = await bookResponse.json();

        let quantityDifference = newQuantity;

        if (orderId) {
            const existingOrderResponse = await fetch(`${API_BASE_URL}/Orders/${orderId}`);
            if (!existingOrderResponse.ok) throw new Error("Failed to fetch existing order details");

            const existingOrder = await existingOrderResponse.json();
            const previousQuantity = existingOrder.quantity;

            quantityDifference = newQuantity - previousQuantity;

            await fetch(`${API_BASE_URL}/Orders/${orderId}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(orderData),
            });

            alert("Order updated successfully!");
        } else {
            await fetch(`${API_BASE_URL}/Orders`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(orderData),
            });

            alert("Order placed successfully!");
        }

        book.purchaseCount += quantityDifference;

        await fetch(`${API_BASE_URL}/Books/${bookId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(book),
        });

        document.getElementById("orderForm").reset();
        document.getElementById("orderId").value = "";
        loadOrders();
        loadBooks();
    } catch (error) {
        console.error("Error saving order:", error);
    }
});

/* Edit Order */
async function editOrder(orderId) {
    try {
        const response = await fetch(`${API_BASE_URL}/Orders/${orderId}`);
        if (!response.ok) throw new Error("Failed to fetch order");

        const order = await response.json();
        
        document.getElementById("orderId").value = order.id;
        document.getElementById("orderCustomerName").value = order.customerName;
        document.getElementById("orderAddress").value = order.address;
        document.getElementById("orderBookId").value = order.bookId;
        document.getElementById("orderQuantity").value = order.quantity;
        
    } catch (error) {
        console.error("Error editing order:", error);
    }
}

/* Delete Order */
async function deleteOrder(orderId) {
    if (!confirm("Are you sure you want to delete this order?")) return;

    try {
        const orderResponse = await fetch(`${API_BASE_URL}/Orders/${orderId}`);
        if (!orderResponse.ok) throw new Error("Failed to fetch order details");

        const order = await orderResponse.json();
        const bookId = order.bookId;
        const quantity = order.quantity;

        console.log("Deleting order:", orderId);
        console.log("Associated book ID:", bookId);
        console.log("Order quantity:", quantity);

        const bookResponse = await fetch(`${API_BASE_URL}/Books/${bookId}`);
        if (!bookResponse.ok) throw new Error("Failed to fetch book details");

        const book = await bookResponse.json();

        book.purchaseCount = Math.max(0, book.purchaseCount - quantity);

        console.log("Updated book purchase count:", book.purchaseCount);

        await fetch(`${API_BASE_URL}/Books/${bookId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(book),
        });

        await fetch(`${API_BASE_URL}/Orders/${orderId}`, { method: "DELETE" });

        alert("Order deleted successfully!");
        loadOrders();
        loadBooks();
    } catch (error) {
        console.error("Error deleting order:", error);
    }
}

/* Reviews Section */
async function loadReviews() {
    const reviewList = document.getElementById("reviewList");
    reviewList.innerHTML = "";

    try {
        const response = await fetch(`${API_BASE_URL}/Reviews`);
        if (!response.ok) throw new Error("Failed to fetch reviews");

        const reviews = await response.json();
        const books = await (await fetch(`${API_BASE_URL}/Books`)).json();
        reviews.forEach(review => {
            const book = books.find(b => b.id === review.bookId);
            const bookTitle = book ? book.title : "Unknown";

            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${review.id}</td>
                <td>${bookTitle}</td>
                <td>${review.customerName}</td>
                <td>${review.content}</td>
                <td>
                    <button class="edit" onclick="editReview(${review.id})">Edit</button>
                    <button class="delete" onclick="deleteReview(${review.id})">Delete</button>
                </td>
            `;
            reviewList.appendChild(row);
        });
    } catch (error) {
        console.error("Error loading reviews:", error);
    }
}

/* Create and Update Review */
document.getElementById("reviewForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const reviewId = document.getElementById("reviewId").value;
    const reviewData = {
        bookId: parseInt(document.getElementById("reviewBookId").value),
        customerName: document.getElementById("reviewCustomerName").value,
        content: document.getElementById("reviewContent").value
    };

    try {
        if (reviewId) {
            await fetch(`${API_BASE_URL}/Reviews/${reviewId}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(reviewData),
            });
            alert("Review updated successfully!");
        } else {
            await fetch(`${API_BASE_URL}/Reviews`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(reviewData),
            });
            alert("Review added successfully!");
        }

        document.getElementById("reviewForm").reset();
        document.getElementById("reviewId").value = "";
        loadReviews();
    } catch (error) {
        console.error("Error saving review:", error);
    }
});

/* Edit Review */
async function editReview(reviewId) {
    try {
        const response = await fetch(`${API_BASE_URL}/Reviews/${reviewId}`);
        if (!response.ok) throw new Error("Failed to fetch review");

        const review = await response.json();
        
        document.getElementById("reviewId").value = review.id;
        document.getElementById("reviewCustomerName").value = review.customerName;
        document.getElementById("reviewContent").value = review.content;
        document.getElementById("reviewBookId").value = review.bookId;

    } catch (error) {
        console.error("Error editing review:", error);
    }
}

/* Delete Review */
async function deleteReview(reviewId) {
    if (!confirm("Are you sure you want to delete this review?")) return;

    try {
        await fetch(`${API_BASE_URL}/Reviews/${reviewId}`, { method: "DELETE" });
        alert("Review deleted successfully!");
        loadReviews();
    } catch (error) {
        console.error("Error deleting review:", error);
    }
}