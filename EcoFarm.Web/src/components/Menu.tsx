import React, { useState } from 'react';
import { AppstoreOutlined, MailOutlined, MenuOutlined, SettingOutlined } from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { Image, Menu } from 'antd';
import logo192 from '../assets/logo192.png'

const items: MenuProps['items'] = [
  // {
  //   key: "logo",
  //   icon: <Image src={logo192} height={70} />
  // },
  {
    label: 'Navigation One',
    key: 'mail',
    icon: <MailOutlined />
  },
  {
    label: 'Navigation Two',
    key: 'app',
    icon: <AppstoreOutlined />,
  },
  {
    label: 'Navigation Three - Submenu',
    key: 'SubMenu',
    icon: <SettingOutlined />,
    children: [
      {
        type: 'group',
        label: 'Item 1',
        children: [
          {
            label: 'Option 1',
            key: 'setting:1',
          },
          {
            label: 'Option 2',
            key: 'setting:2',
          },
        ],
      },
      {
        type: 'group',
        label: 'Item 2',
        children: [
          {
            label: 'Option 3',
            key: 'setting:3',
          },
          {
            label: 'Option 4',
            key: 'setting:4',
          },
        ],
      },
    ],
  },
  {
    label: (
      <a href="https://ant.design" target="_blank" rel="noopener noreferrer">
        Navigation Four - Link
      </a>
    ),
    key: 'alipay',
  },
];

const MenuBar: React.FC = () => {
  const [current, setCurrent] = useState('mail');

  const onClick: MenuProps['onClick'] = (e) => {
    console.log('click ', e);
    setCurrent(e.key);
  };

  return (
    <div>
      <Image 
        width={250}
        src={logo192}
      />
      <Menu 
      onClick={onClick} 
      selectedKeys={[current]} 
      mode="horizontal" 
      items={items} 
      expandIcon={<MenuOutlined />}
      />
    </div>
  );
};

export default MenuBar;