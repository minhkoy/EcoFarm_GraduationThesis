import React, { useRef, useState } from 'react';
import logo from '../logo.svg';
import { Button, Form, Image, Select } from 'antd';
import MenuBar from '../components/Menu';
import { Footer } from 'antd/es/layout/layout';

interface Country {
  name?: string;
  value?: string;
  cities?: Array<string>;
}

function Home () {
  // let count = useRef(0);
  // let [clicked, setClicked] = useState(false);

  // const checkRerender = () => {
  //   setClicked(!clicked)
  //   //if (clicked) 
  //   count.current++;
  // }

  return (
  <div>
    Hello world!!!
  </div>
  )
}

export default Home