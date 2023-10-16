import React from 'react';
import Container from '@mui/material/Container';
import { AppBar, Toolbar, Button, Typography } from '@mui/material';
import { Link } from 'react-router-dom'; // Import Link for routing

function Navigation() {
  return (
    <AppBar position="static">
      <Container maxWidth="xl">
      <Toolbar disableGutters>
      <Typography
            variant="h6"
            noWrap
            component="a"
            href="#app-bar-with-responsive-menu"
            sx={{
              mr: 2,
              display: { xs: 'none', md: 'flex' },
              fontFamily: 'monospace',
              fontWeight: 700,
              letterSpacing: '.3rem',
              color: 'inherit',
              textDecoration: 'none',
            }}
          >
            TODO
          </Typography>
        <Button component={Link} to="/" color="inherit">Home</Button>
        <Button component={Link} to="/table" color="inherit">Table</Button>
      </Toolbar>
      </Container>
    </AppBar>
  );
}

export default Navigation;