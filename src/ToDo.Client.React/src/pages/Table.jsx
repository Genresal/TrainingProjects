import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import { useEffect, useState } from 'react';
import { createItem, deleteItem, fetchData, updateItem } from '../api/todo.api';

function createData(id, name, isComplete) {
  return { id, name, isComplete };
}

const moqData = [
  createData(1, 'Frozen yoghurt', true),
  createData(2, 'Ice cream sandwich', false),
];

export default function DenseTable() {
    const [data, setData] = useState([]);
    const [storedItem, setStoredItem] = useState({id: 0,  name: '', isComplete: false }); // State for the item

    useEffect(() => {
        const fetchDataFromAPI = async () => {
            try {
              const result = await fetchData(); // Assuming fetchData is an async function

              if (result == null || result.count === 0) {
                console.log('Loaded from mocked data');
                setData(moqData); // Use the preset collection if result is null or empty
            } else {
                console.log('Loaded from api');
                setData(result);
            }
            } catch (error) {
              console.error('Error during collection fetching:', error);
              setData(moqData); 
            }
          };

        fetchDataFromAPI();
    }, []);

    // Function to handle adding a new item
    const handleAddItem = async () => {
        try {
            const response = await createItem(storedItem);
            setData([...data, response]); // Add the new item to your data
            setStoredItem({id: 0, name: '', isComplete: false }); // Clear the form
        } catch (error) {
            console.error('Error during item addition:', error);
        }
    }

    // Function to handle updating an item
    const handleUpdateItem = async () => {
        try {
            const response = await updateItem(storedItem.id, storedItem);
            setData(data.map(item => (item.id === storedItem.id ? response : item))); // Update the item in your data
            setStoredItem({id: 0, name: '', isComplete: false }); // Clear the form
        } catch (error) {
            console.error('Error during item update:', error);
        }
    }

    // Function to handle deleting an item
    const handleDeleteItem = async (id) => {
        try {
            // Send a request to delete the item using the deleteItem function
            await deleteItem(id);
        
            // Update your data state to remove the deleted item
            setData(data.filter(item => item.id !== id));
        } catch (error) {
            console.error('Error during item deletion:', error);
        }
    };

    // Function to handle adding a new item
    const handleRowClick = async (id, name, isComplete) => {
        setStoredItem({id, name, isComplete}); // Clear the form
    }

  return (
    <Box sx={{ width: '100%' }}>
    <Paper sx={{ width: '100%', mb: 2 }}>
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
        <TableHead>
          <TableRow>
            <TableCell>Id</TableCell>
            <TableCell>Name of TODO item</TableCell>
            <TableCell align="right">Is completed</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((row) => (
            <TableRow key={row.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
              <TableCell  onClick={() => handleRowClick(row.id, row.name, row.isComplete)}  component="th" scope="row">{row.id}</TableCell>
              <TableCell component="th" scope="row">{row.name}</TableCell>
              <TableCell align="right">{row.isComplete.toString()}</TableCell>
              <TableCell>
                {/* Button to delete the item */}
                <button onClick={() => handleDeleteItem(row.id)}>Delete</button>
        </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
     </Paper>

     <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start' }}>
     <Paper sx={{ width: '50%', mb: 2}}>
                {/* Form for adding a new item */}
                <div>
                    <label>Name:</label>
                    <input
                        type="text"
                        value={storedItem.name}
                        onChange={(e) => setStoredItem({ ...storedItem, name: e.target.value })}
                    />
                </div>
                <div>
                    <label>Is Completed:</label>
                    <input
                        type="checkbox"
                        checked={storedItem.isComplete}
                        onChange={(e) => setStoredItem({ ...storedItem, isComplete: e.target.checked })}
                    />
                </div>
                <button onClick={handleAddItem}>Add Item</button>
                <button onClick={handleUpdateItem}>Update Item</button>
                </Paper>
            </Box>
    </Box>
  );
}