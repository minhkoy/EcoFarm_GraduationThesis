// import {
//   IconBellRinging,
//   IconFingerprint,
//   IconKey,
//   IconSettings,
//   Icon2fa,
//   IconDatabaseImport,
//   IconReceipt2,
//   IconSwitchHorizontal,
//   IconLogout,
// } from '@tabler/icons-react';
// import { MantineLogo } from '@mantinex/mantine-logo';
import { Box } from '@mantine/core';
import { HomeIcon, ListOrderedIcon, LogOutIcon, PersonStandingIcon, PiggyBankIcon } from 'lucide-react';
import Link from 'next/link';
import { useRouter } from 'next/router';
//import classes from './NavbarSimpleColored.module.css';

const data = [
  { link: '/user/profile', label: 'Thông tin cá nhân', icon: <PersonStandingIcon className='inline' /> },
  { link: '/user/profile/addresses', label: 'Địa chỉ', icon: <HomeIcon className='inline' /> },
  { link: '/user/profile/orders', label: 'Đơn hàng của bạn', icon: <ListOrderedIcon className='inline' /> },
  { link: '/user/profile/your-packages', label: 'Các gói farming bạn đăng ký', icon: <PiggyBankIcon className='inline' /> },
];

export function UserInfo() {
  //const [active, setActive] = useState('Thông tin cá nhân');
  const router = useRouter();
  const links = data.map((item) => (
    <Link
      //className={classes.link}
      data-active={item.link === (router.pathname)}
      href={item.link}
      key={item.label}
      // onClick={(event) => {
      //   //event.preventDefault();
      //   console.log(item.label);
      //   //setActive(item.label);
      // }}
      className='hover:bg-orange-300 p-4 pl-0 data-[active=true]:bg-teal-300'
    >
      <div> {item.icon} {item.label}</div>
    </Link>
  ));

  return (
    <nav className='h-full flex flex-col gap-5'>
      <div className='flex flex-col gap-5'>
        {/* <Group className={classes.header} justify="space-between">
          <MantineLogo size={28} inverted style={{ color: 'white' }} />
          <Code fw={700} className={classes.version}>
            v3.1.2
          </Code>
        </Group> */}
        {links}
      </div>

      <div>

        {/* <a href="#" className={classes.link} onClick={(event) => event.preventDefault()}>
          < className={classes.linkIcon} stroke={1.5} />
          <span>Change account</span>
        </a> */}
        <Box className='hover:bg-orange-300' p={4} pl={0}>
          <a href="#" onClick={(event) => event.preventDefault()}
          >
            <span><LogOutIcon className='inline' /> Đăng xuất</span>
          </a>
        </Box>
      </div>
    </nav>
  );
}