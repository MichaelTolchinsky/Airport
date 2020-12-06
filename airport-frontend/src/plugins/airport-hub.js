import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

let connection;
let connectionPromise;

async function start() {
  try {
    return await connection.start();
  } catch (err) {
    console.error("Failed to connect with hub", err);
    return new Promise((resolve, reject) =>
      setTimeout(async () => {
        try {
          const con = await start();
          resolve(con);
        } catch (error) {
          reject();
        }
      }, 5000)
    );
  }
}

export const invokeHub = (methodName, ...args) =>
  connection.invoke(methodName, ...args);

export const registerAirportHubListener = (methodName, callback) =>
  connectionPromise.then(() => connection.on(methodName, callback));

export const unregisterAirportHubListener = (methodName, callback) =>
  connectionPromise.then(() =>
    callback ? connection.off(methodName, callback) : connection.off(methodName)
  );

export default {
  install: (app) => {
    connection = new HubConnectionBuilder()
      .withUrl("http://localhost:53109/AirportHub")
      .configureLogging(LogLevel.Information)
      .build();
    app.provide("AirportHub", connection);
    connection.onclose(() => start());
    connectionPromise = start();
  },
};
