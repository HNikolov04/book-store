const API_BASE_URL = "https://localhost:7122/api";

document.addEventListener("DOMContentLoaded", () => {
    fetchBookDetails();
    setupPurchaseForm();
    setupReviewForm();
});

async function fetchBookDetails() {
    const urlParams = new URLSearchParams(window.location.search);
    const bookId = urlParams.get("id");

    if (!bookId) {
        alert("No book selected.");
        window.location.href = "/client/html/index.html";
        return;
    }

    try {
        const response = await fetch(`${API_BASE_URL}/Books/${bookId}`);
        if (!response.ok) throw new Error("Failed to fetch book details");

        const book = await response.json();
        displayBookDetails(book);
        loadReviews(bookId);
    } catch (error) {
        console.error("Error fetching book details:", error);
        document.querySelector(".book-page").innerHTML = "<p>Failed to load book details.</p>";
    }
}

function displayBookDetails(book) {
    document.getElementById("bookImage").src = book.imageUrl;
    document.getElementById("bookImage").alt = book.title;
    document.getElementById("bookTitle").textContent = book.title;
    document.getElementById("bookShortDescription").textContent = book.shortDescription;
    document.getElementById("bookFullDescription").textContent = book.fullDescription;
    document.getElementById("bookPrice").textContent = `$${book.price}`;
    document.getElementById("bookId").value = book.id;
}

async function loadReviews(bookId) {
    const reviewsContainer = document.getElementById("reviewsContainer");
    reviewsContainer.innerHTML = "";

    try {
        const response = await fetch(`${API_BASE_URL}/Reviews`);
        if (!response.ok) throw new Error("Failed to fetch reviews");

        const reviews = await response.json();

        const filteredReviews = reviews.filter(review => review.bookId === parseInt(bookId));

        if (filteredReviews.length === 0) {
            reviewsContainer.innerHTML = "<p>No reviews yet.</p>";
            return;
        }

        filteredReviews.forEach(review => {
            const reviewElement = document.createElement("p");
            reviewElement.innerHTML = `<strong>${review.customerName}:</strong> "${review.content}"`;
            reviewsContainer.appendChild(reviewElement);
        });
    } catch (error) {
        console.error("Error loading reviews:", error);
        reviewsContainer.innerHTML = "<p>Failed to load reviews.</p>";
    }
}

function setupPurchaseForm() {
    document.getElementById("purchaseForm").addEventListener("submit", async function (event) {
        event.preventDefault();

        const bookId = document.getElementById("bookId").value;
        const quantity = parseInt(document.getElementById("orderQuantity").value);

        const order = {
            bookId: bookId,
            customerName: document.getElementById("customerName").value,
            address: document.getElementById("customerAddress").value,
            quantity: quantity,
        };

        try {
            console.log("Placing order for book ID:", bookId, "with quantity:", quantity);

            const bookResponse = await fetch(`${API_BASE_URL}/Books/${bookId}`);
            if (!bookResponse.ok) throw new Error("Failed to fetch book details");

            const book = await bookResponse.json();

            book.purchaseCount += quantity;

            console.log("Updated book purchase count:", book.purchaseCount);

            const response = await fetch(`${API_BASE_URL}/Orders`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(order),
            });

            if (!response.ok) throw new Error("Failed to place order");

            await fetch(`${API_BASE_URL}/Books/${bookId}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(book),
            });

            alert("Order placed successfully!");
            this.reset();
        } catch (error) {
            console.error("Error placing order:", error);
            alert("Failed to place order. Please try again.");
        }
    });
}

function setupReviewForm() {
    document.getElementById("reviewForm").addEventListener("submit", async function (event) {
        event.preventDefault();

        const bookId = document.getElementById("bookId").value;
        const reviewContent = document.getElementById("review").value.trim();

        if (!reviewContent) {
            alert("Please enter a review before submitting.");
            return;
        }

        const review = {
            bookId,
            customerName: "Anonymous",
            content: reviewContent
        };

        try {
            const response = await fetch(`${API_BASE_URL}/Reviews`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(review),
            });

            if (!response.ok) throw new Error("Failed to submit review");

            alert("Review submitted!");
            document.getElementById("review").value = "";
            loadReviews(bookId);
        } catch (error) {
            console.error("Error submitting review:", error);
            alert("Failed to submit review. Please try again.");
        }
    });
}