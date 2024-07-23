import React, { useEffect, useState } from 'react';
import { makeStyles } from '@mui/styles';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Alert from '@mui/material/Alert';
import { useNavigate,useParams  } from 'react-router-dom';
import { GET_ALL_CATEGORIES, GET_PRODUCT_ID, PUT_EDIT_PRODUCT } from '../api/apiService';

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

const EditProduct = ({ location }) => {
  const classes = useStyles();
  const [checkUpdate, setCheckUpdate] = useState(false);
  const { id: idProduct } = useParams(); 
  const [title, setTitle] = useState(null);
  const [slug, setSlug] = useState(null);
  const [body, setBody] = useState(null);
  const [category, setCategory] = useState(0);
  const [categories, setCategories] = useState([]);
  const navigate = useNavigate();
  useEffect(() => {
    const fetchData = async () => {
      try {
        const product = await GET_PRODUCT_ID('Product', idProduct);
        setTitle(product.data.title);
        setBody(product.data.body);

        setSlug(product.data.slug);

        setCategory(product.data.category.idCategory);

        const categoryData = await GET_ALL_CATEGORIES('Category');
        setCategories(categoryData.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, [idProduct]);

  const handleChangeTitle = (event) => setTitle(event.target.value);
  const handleChangeSlug = (event) => setSlug(event.target.value);
  const handleChangeBody = (event) => setBody(event.target.value);
  const handleChangeCategory = (event) => setCategory(event.target.value);

  const handleEditProduct = async (event) => {
    event.preventDefault();

    if (title !== '' && body !== ''  && category > 0 && idProduct > 0 && slug !== '') {
      const product = {
        title: title,

        slug: slug,

        body: body,
        
        idCategory: category,

        category:{
          idCategory: category,
          name: "string",
          slugCategory: "string",
          products:[]
        }
      };

      try {
        const editedProduct = await PUT_EDIT_PRODUCT(`Product/${idProduct}`, product);

        if (editedProduct.data === 1) {
          setCheckUpdate(true);
        } else {
          alert('Bạn chưa nhập đủ thông tin!');
        }
      } catch (error) {
        console.error('Error editing product:', error);
      }
    }
  };

  useEffect(() => {
    if (checkUpdate) {
      navigate('/');
    }
  }, [checkUpdate]);
return (
    <div className={classes.root}>
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Paper className={classes.paper}>
            <Typography className={classes.title} variant="h4">
              Edit Product
            </Typography>
            <Grid item xs={12} container>
              <Grid item xs={12}>
                <Typography gutterBottom variant="subtitle1">
                  Title
                </Typography>
                <TextField
                  id="Title"
                  onChange={handleChangeTitle}
                  value={title || ''}
                  name="Title"
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
                  defaultValue={body || ''}
                  name="Body"
                  className={classes.txtInput}
                  multiline
                  rows={4}
                  variant="outlined"
                />
              </Grid>
              <Grid item xs={12}>
                <Typography gutterBottom variant="subtitle1">
                  Slug
                </Typography>
                <TextField
                  id="Slug"
                  onChange={handleChangeSlug}
                  value={slug || ''}
                  name="Slug"
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
                  value={category || 0}
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
                  onClick={handleEditProduct}
                  fullWidth
variant="contained"
                  color="primary"
                  className={classes.submit}
                >
                  Update product
                </Button>
              </Grid>
            </Grid>
          </Paper>
        </Grid>
      </Grid>
    </div>
  );
};

export default EditProduct;