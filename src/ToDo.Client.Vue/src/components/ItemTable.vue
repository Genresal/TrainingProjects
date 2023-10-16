<script>
import { createItem, deleteItem, fetchData, updateItem } from '../api/todo.api';

export default {
  data() {
    return {
      items: [], // Store the list of items
      storedItem: {
        id: null,
        name: '',
        isComplete: false,
      },
    };
  },
  mounted() {
    // Fetch data when the component is mounted (loaded)
    const result = fetchData();
    
    console.log('Loaded from api');
    items = result;
  },
  methods: {
    async handleAddItem() {
        try {
            const response = await createItem(storedItem);
            this.items.push(response); // Add the new item to your data
            this.newItem = {id: 0, name: '', isComplete: false }; // Clear the form
        } catch (error) {
            console.error('Error during item addition:', error);
        }
    },
    async handleUpdateItem() {
        try {
            const response = await updateItem(storedItem.id, storedItem);
                // Update the item in your data using map
                this.items = this.items.map((item) => {
                  if (item.id === this.editItem.id) {
                    return response.data;
                  }
                  return item;
                });
            this.newItem = {id: 0, name: '', isComplete: false };  // Clear the form
        } catch (error) {
            console.error('Error during item update:', error);
        }
    },
    async handleDeleteItem(id) {
        try {
            // Send a request to delete the item using Axios or your API service
            await deleteItem(id); // Adjust the URL and API endpoint as needed

            // Update your data state to remove the deleted item using filter
            this.items = this.items.filter((item) => item.id !== id);
        } catch (error) {
            console.error('Error during item deletion:', error);
        }
    },
    handleRowClick(item) {
      this.storedItem = item
    },
  },
};
</script>

<template>
  <div>
    <table>
      <!-- Render your table rows here -->
      <tr v-for="item in items" :key="item.id">
        <td>{{ item.id }}</td>
        <td>{{ item.name }}</td>
        <td>{{ item.isComplete }}</td>
        <td>
          <button @click="edit(item)">Edit</button>
          <button @click="deleteItem(item.id)">Delete</button>
        </td>
      </tr>
    </table>

    <form @submit.prevent="createItem">
      <input v-model="newItem.name" placeholder="Name">
      <input type="checkbox" v-model="newItem.isComplete">
      <button type="submit">Create</button>
    </form>

    <form @submit.prevent="updateItem">
      <input v-model="editItem.name" placeholder="Name">
      <input type="checkbox" v-model="editItem.isComplete">
      <button type="submit">Update</button>
    </form>
  </div>
</template>