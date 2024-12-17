document.addEventListener("DOMContentLoaded", function () {
    const chatbotIcon = document.getElementById("chatbot-icon");
    const chatbot = document.getElementById("chatbot");
    const chatbotClose = document.getElementById("chatbot-close");
    const chatbotMessages = document.getElementById("chatbot-messages");
    const chatbotInput = document.getElementById("chatbot-question");
    const chatbotSend = document.getElementById("chatbot-send");

    // Mở chatbot khi nhấn vào icon
    chatbotIcon.addEventListener("click", () => {
        chatbot.style.display = "flex";
        chatbotIcon.style.display = "none";
    });

    // Đóng chatbot khi nhấn nút đóng
    chatbotClose.addEventListener("click", () => {
        chatbot.style.display = "none";
        chatbotIcon.style.display = "block";
    });

    // Gửi tin nhắn khi nhấn nút "Gửi" hoặc nhấn Enter
    chatbotSend.addEventListener("click", sendMessage);
    chatbotInput.addEventListener("keypress", function (e) {
        if (e.key === "Enter") sendMessage();
    });

    function sendMessage() {
        const userInput = chatbotInput.value.trim();
        if (!userInput) return;

        // Hiển thị tin nhắn của người dùng
        addMessage("user", userInput);

        // Xóa input sau khi gửi
        chatbotInput.value = "";

        // Gửi yêu cầu đến server
        fetch("/Chatbot/GetChatResponse", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ userInput: userInput }),
        })
            .then((response) => response.json())
            .then((data) => {
                if (data.response) {
                    addMessage("bot", data.response);
                } else {
                    addMessage("bot", "Xin lỗi, tôi không thể trả lời ngay bây giờ.");
                }
            })
            .catch((error) => {
                console.error("Lỗi:", error);
                addMessage("bot", "Có lỗi xảy ra, vui lòng thử lại.");
            });
    }

    function addMessage(sender, text) {
        const messageDiv = document.createElement("div");
        messageDiv.classList.add(sender);
        messageDiv.textContent = text;
        chatbotMessages.appendChild(messageDiv);

        // Tự động cuộn xuống cuối
        chatbotMessages.scrollTop = chatbotMessages.scrollHeight;
    }
});
