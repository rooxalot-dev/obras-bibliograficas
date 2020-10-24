interface MenuItem {
  icon?: string,
  name: string,
  route: string;
}

export const menuItems: MenuItem[] = [
  { name: 'Home', route: '/' },
  { name: 'Autores', route: '/authors' },
];
