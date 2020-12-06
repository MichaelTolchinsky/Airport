<template>
  <h2>{{ title }}</h2>
  <div class="table-container">
    <div class="table-row header">
      <span>Station</span>
      <span>Current Flight</span>
    </div>
    <div class="table-row" v-for="st in stations" :key="st.id">
      <span>{{ st.id }}</span>
      <span>{{ st.currentFlightId }}</span>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    title: {
      type: String,
      required: true,
    },
    stations: {
      type: Array,
      required: true,
    },
  },
};
</script>
<style lang="scss" scoped>
.table-container {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;

  .table-row {
    border: 1px solid #dddddd;
    text-align: left;

    display: flex;
    justify-content: space-evenly;

    & > span {
      width: 100%;
      padding: 8px;
      &:not(:first-of-type) {
        border-left: 2px solid #c4c4c4;
      }
    }

    &.header {
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