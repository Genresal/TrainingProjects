<template>
    <div>
      <h1>Table Page</h1>
      <ItemTable />
    </div>
  </template>

<script>
import ItemTable from '@/components/ItemTable.vue'; // Update the import path

export default {
  components: {
    ItemTable,
  },
};
</script>