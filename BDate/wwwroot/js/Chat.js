﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

// Receiving messages
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

// As I understand, creates a unique Id which is mandatory to use SignalR
connection.start().then(function () {
    connection.invoke("GetConnectionId").then(function (id) {
        document.getElementById("connectionId").innerText = id;
    });
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// Send Message button
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var room = document.getElementById("room").value;
    connection.invoke("SendMessage", user, message, room, false).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// Join Room button
document.getElementById("joinButton").addEventListener("click", function (event) {

    var room = document.getElementById("room").value;
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message, room, true).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});