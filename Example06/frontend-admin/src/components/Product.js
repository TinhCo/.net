import React, { useEffect, useState } from 'react';
import { makeStyles } from '@mui/styles';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';
import {  GET_ALL_CATEGORIES, POST_ADD_PRODUCT } from '../api/apiService';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    marginTop: 20,
  },
  paper: {
    padding: theme.spacing(2),
    margin: 'auto',
    maxWidth: 600,
  },
  title: {
    fontSize: 30,
    textAlign: 'center',
  },
  txtInput: {
    width: '98%',
    margin: '10px',
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

export default function Product() {
  const classes = useStyles();
  const [checkAdd, setCheckAdd] = useState(false);
  const [title, setTitle] = useState('');
  const [slug, setSlug] = useState('');
  const [body, setBody] = useState('');
  const [category, setCategory] = useState(0);
  const [categories, setCategories] = useState([]);
  const navigate = useNavigate();
  useEffect(() => {
    GET_ALL_CATEGORIES('Category').then((item) => {
      setCategories(item.data);
    });
  }, []);

  const handleChangeTitle = (event) => {
    setTitle(event.target.value);
  };

  const handleChangeBody = (event) => {
    setBody(event.target.value);
  };
  const handleChangeSlug = (event) => {
    setSlug(event.target.value);
  };

  const handleChangeCategory = (event) => {
    setCategory(event.target.value);
  };

  const handleAddProduct = (event) => {
    event.preventDefault();

    if (title !== '' && body !== ''  && category > 0 && slug !== '') {
      const product = {
        title: title,
        //
        slug: slug,
        //
        body: body,
        idCategory: category,
      };

      POST_ADD_PRODUCT('Product', product).then((item) => {
        if (item.data === 1) {
          setCheckAdd(true);
        } else {
            alert('Bạn chưa nhập đủ thông tin!');
        }
      });
    }else{
        alert('Bạn chưa nhập đủ thông tin!');
    }
  };

  useEffect(() => {
    if (checkAdd) {
      navigate('/');
    }
  }, [checkAdd]);

  return (
    <div className={classes.root}>
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Paper className={classes.paper}>
            <Typography className={classes.title} variant="h4">
              Add Product
            </Typography>
            <Grid item container>
              <Grid item xs={12}>
                <Typography gutterBottom variant="subtitle1">
                  Title
                </Typography>
                <TextField
                  id="Title"
                  onChange={handleChangeTitle}
                  name="Title"
                  label="Title"
                  variant="outlined"
                  className={classes.txtInput}
size="small"
/>
              </Grid>
              <Grid item xs={12}>
                <Typography gutterBottom variant="subtitle1">
                  Body
                </Typography>
                <TextField
                  id="outlined-multiline-static"
                  onChange={handleChangeBody}
                  label="Body"
                  name="Body"
                  className={classes.txtInput}
                  multiline
                  rows={4}
                  variant="outlined"
                />
              </Grid>
              <Grid item xs={12}>
                <Typography gutterBottom variant="subtitle1">
                  Title
                </Typography>
                <TextField
                  id="Slug"
                  onChange={handleChangeSlug}
                  name="Slug"
                  label="Slug"
                  variant="outlined"
                  className={classes.txtInput}
                  size="small"
                />
              </Grid>
              <Grid item xs={12}>
                <Typography gutterBottom variant="subtitle1">
                  Choose Category
                </Typography>
                <TextField
                  id="outlined-select-currency-native"
                  name="idCategory"
                  select
                  value={category}
                  onChange={handleChangeCategory}
                  SelectProps={{
                    native: true,
                  }}
                  helperText="Please select your category"
                  variant="outlined"
                  className={classes.txtInput}
                >
                  <option value="0">Choose category</option>
                  {categories.length > 0 &&
                    categories.map((option) => (
                      <option key={option.idCategory} value={option.idCategory}>
                        {option.name}
                      </option>
                    ))}
                </TextField>
              </Grid>
              <Grid item xs={12}>
                <Button
                  type="button"
                  onClick={handleAddProduct}
                  fullWidth
                  variant="contained"
                  color="primary"
                  className={classes.submit}
                >
                  Add product
                </Button>
              </Grid>
            </Grid>
          </Paper>
        </Grid>
      </Grid>
    </div>
  );
}