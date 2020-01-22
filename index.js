const express = require('express')
const path = require('path')

require('dotenv').config()

const scraping = require('./services/scraping')

const baseUrl = 'https://cineplayers.com'
const cpUser = 12688

const app = express();
const port = process.env.PORT || "3000";

app.set('view engine', 'ejs')
app.use(express.static('public'))
app.use('/styles/bulma.min.css', express.static('node_modules/bulma/css/bulma.min.css'))
app.use('/styles', express.static('node_modules/@fortawesome/fontawesome-free/css'))
app.use('/styles/fa.min.css', express.static('node_modules/@fortawesome/fontawesome-free/css/all.min.css'))
app.use('/scripts/fa.min.js', express.static('node_modules/@fortawesome/fontawesome-free/js/all.min.js'))

app.use(require('./api/routes/index'));

app.listen(port, () => {
    console.log(`Listening to requests on http://localhost:${port}`);
});

//scraping.importWatchList(baseUrl, cpUser)
