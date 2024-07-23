import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { Container } from '@mui/material';
import CssBaseline from '@mui/material/CssBaseline';
import MenuTop from './components/MenuTop';
import Home from './components/Home';
import Product from './components/Product';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import EditProduct from './components/EditProduct';
// import Category from './components/Category';
const theme = createTheme();

const App = () => {
  return (
    <ThemeProvider theme={theme}>
    <React.Fragment>
      <CssBaseline />
      <MenuTop />
      <Container maxWidth="md">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/home" element={<Home />} />
          <Route path="/products" element={<Product />} />
          <Route path="/edit/product/:id" element={<EditProduct />} />
          {/* <Route path="/categories" element={<Category />} /> */}

        </Routes>
      </Container>
    </React.Fragment>
    </ThemeProvider>
  );
};

export default App;