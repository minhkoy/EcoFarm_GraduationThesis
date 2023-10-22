import React, { useRef, useState } from 'react';
import logo from '../logo.svg';
import { Button, Image } from 'antd';
import MenuBar from '../components/Menu';
import { Footer } from 'antd/es/layout/layout';
function Home () {
  let count = useRef(0);
  let [clicked, setClicked] = useState(false);

  const testTailwind = () => {
    for (let i = 1; i <= 9; i++) {
      return <div>{i}</div>
    }
  }
  const checkRerender = () => {
    setClicked(!clicked)
    //if (clicked) 
    count.current++;
  }

  return (
  <div className="App bg-red-400 flex flex-col">
    <div className="flex flex-row">
      <div className="bg-blue-500 flex-grow">
        <MenuBar />
      </div>
      <div className="bg-green-500">
        <Button type='default' onClick={checkRerender}>Hello Ant? {count.current} times</Button>
        <Image src='https://cdnimg.vietnamplus.vn/uploaded/ngtmbh/2023_05_31/thi_tran_hoang_hon_2.jpg' 
        width={500}/>
      </div>
    </div>
    
    {/* <div className="grid grid-rows-3 grid-flow-col gap-4">
      <div>1</div>
      <div>2</div>
      <div>3</div>
    </div> */}
    <Footer className='bg-red-400'>
      <span>Alo </span>
    </Footer>
  </div>
  )
}

export default Home