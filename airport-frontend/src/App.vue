<template>
  <div class="parent">
    <div class="div1">
      <station-table title="Stations" :stations="stations"></station-table>
    </div>
    <div class="div2">
      <flight-table
        title="Landing Flights"
        :flights="landingFlights"
      ></flight-table>
    </div>
    <div class="div3">
      <flight-table
        title="Takeoff Flights"
        :flights="takeoffFlights"
      ></flight-table>
    </div>
    <div class="div4"></div>
  </div>
</template>

<script>
import FlightTable from "@/components/FlightTable";
import {
  registerAirportHubListener,
  unregisterAirportHubListener,
} from "@/plugins/airport-hub";
import axios from "axios";
import StationTable from "./components/StationTable.vue";

export default {
  components: { FlightTable, StationTable },
  data() {
    return {
      landingFlights: [],
      takeoffFlights: [],
      stations: [],
    };
  },
  methods: {
    updateFlightAtStation(flight, station) {
      const stationEl = this.stations.find((st) => st.id === station.id);
      if(stationEl){
        stationEl.currentFlightId = flight?.id;
      }
    },
    listenToDataSentToMe() {
      registerAirportHubListener(
        "FlightMoved",
        ({ stationFrom, stationTo, flight }) => {
          if (!stationFrom) {
            if (flight.flightDirection === 0) {
              this.landingFlights = this.landingFlights.filter(
                (f) => f.id !== flight.id
              );
            } else {
              this.takeoffFlights = this.takeoffFlights.filter(
                (f) => f.id !== flight.id
              );
            }
            this.updateFlightAtStation(flight, stationTo);
          } else if (!stationTo) {
            this.updateFlightAtStation(null, stationFrom);
          } else {
            this.updateFlightAtStation(null, stationFrom);
            this.updateFlightAtStation(flight, stationTo);
          }
        }
      );
      registerAirportHubListener("NewFlight", (flight) => {
        if (flight.flightDirection === 0) {
          this.landingFlights.push(flight);
        } else {
          this.takeoffFlights.push(flight);
        }
      });
    },
    unlistenToDataSentToMe() {
      unregisterAirportHubListener("FlightMoved");
    },
  },
  created() {
    axios.get("http://localhost:53109/api/Airport/GetData").then((data) => {
      console.log(data);
      const { landingFlights, takeoffFlights, stations } = data.data;
      this.landingFlights = landingFlights;
      this.takeoffFlights = takeoffFlights;
      this.stations = stations;
    });
    this.listenToDataSentToMe();
  },
  unmounted() {
    this.unlistenToDataSentToMe();
  },
};
</script>

<style lang="scss">
* {
  box-sizing: border-box;
}
html,
body {
  height: 100%;
  overflow: hidden;
}

#app {
  height: 100%;
}
</style>
<style lang="scss" scoped>
.parent {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-template-rows: repeat(3, 1fr);
  grid-column-gap: 25px;
  grid-row-gap: 25px;
  margin: 20px;
  padding: 20px;
}

.div1 {
  grid-area: 1 / 2 / 3 / 3;
}
.div2 {
  grid-area: 1 / 1 / 2 / 2;
}
.div3 {
  grid-area: 2 / 1 / 3 / 2;
}
.div4 {
  grid-area: 3 / 1 / 4 / 3;
}
</style>
