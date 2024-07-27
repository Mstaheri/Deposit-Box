var chatBox = $("#ChatBox");

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start();



//نمایش چت باکس برای کاربر
function showChatDialog() {
    chatBox.css("display", "block");
}

function Init() {

    setTimeout(showChatDialog, 1000);


    // هر زمان که دکمه ارسال در چت باکس کلیک شور کد های زیر اجرا می شود
    var NewMessageForm = $("#NewMessageForm");
    NewMessageForm.on("submit", function (e) {

        e.preventDefault();
        var message = e.target[0].value;
        e.target[0].value = '';
        sendMessage(message);
    });

}

//ارسال پیام به سرور
function sendMessage(text) {
    connection.invoke('SendNewMessage', " بازدید کننده ", text);
}

//درسافت پیام از سرور
connection.on('getNewMessage', getMessage);

function getMessage(sender, message, time) {

    $("#Messages").append("<li><div><span class='name'>" + sender + "</span><span class='time'>" + time + "</span></div><div class='message'>" + message + "</div></li>")
};


$(document).ready(function () {
    Init();
});
