const API_BASE_URL = "https://localhost:7122/api";

document.addEventListener("DOMContentLoaded", fetchBooks);

async function fetchBooks() {
    try {
        const response = await fetch(`${API_BASE_URL}/Books`);
        if (!response.ok) throw new Error("Failed to fetch books");

        const books = await response.json();
        displayBooks(books);
    } catch (error) {
        console.error("Error fetching books:", error);
    }
}

function displayBooks(books) {
    const bookList = document.querySelector(".book-list");
    bookList.innerHTML = "";

    books.forEach(book => {
        const bookElement = document.createElement("div");
        bookElement.classList.add("book");

        bookElement.innerHTML = `
            <img src="${book.imageUrl}" alt="${book.title}">
            <h2>${book.title}</h2>
            <p class="short-desc">${book.shortDescription}</p>
            <p class="price">$${book.price.toFixed(2)}</p>
            <a href="/client/html/book.html?id=${book.id}">Order</a>
        `;

        bookList.appendChild(bookElement);
    });
}