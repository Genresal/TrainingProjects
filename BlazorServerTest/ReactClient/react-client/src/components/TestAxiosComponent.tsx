import React, { useState, useEffect } from "react";
import axios from "axios";

const TestAxiosComponent = () => {
  const [data, setData] = useState<any[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      const result = await axios.get<any[]>("http://localhost:5190/api/recipe");
      console.log(result.data);
      setData(result.data);
    };

    fetchData();
  }, []);

  return (
    <ul>
      {data.map(item => (
        <li key={item.id}>{item.Name}</li>
      ))}
    </ul>
  );
};

export default TestAxiosComponent;