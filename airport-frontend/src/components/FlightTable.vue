<template>
  <h2>{{ title }}</h2>
  <div class="table-container">
    <div class="table-row header">
      <span>Flight</span>
      <span>Time</span>
    </div>
    <transition-group name="list">
      <div class="table-row" v-for="flight in flights" :key="flight.id">
      <span>{{ flight.id }}</span>
      <span>{{ printTime(flight.scheduledTime) }}</span>
    </div>
    </transition-group>
    
  </div>
</template>

<script>
export default {
  props: {
    title: {
      type: String,
      required: true,
    },
    flights: {
      type: Array,
      required: true,
    },
  },
  methods: {
    printTime(time) {
      return new Date(time).toLocaleString();
    },
  },
};
</script>

<style lang="scss" scoped>
.table-container {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
  max-height: 25vh;
  overflow: auto;
  
  .list-enter-active,
  .list-leave-active {
    transition: 450ms ease-in;
  }

  .list-enter-from,
  .list-leave-to {
    opacity: 0;
  }

  .table-row {
    border: 1px solid #dddddd;
    text-align: left;
    display: flex;
    justify-content: space-evenly;

    & > span{
      width: 100%;
      padding: 8px;
      &:not(:first-of-type){
      border-left: 2px solid #c4c4c4;
      }
    }

    &.header{
      position: sticky;
      top: 0;
      background: #fff;
      font-weight: bold;
    }

    &:not(.header):nth-child(even) {
      background-color: #dddddd;
    }
  }
}
</style>