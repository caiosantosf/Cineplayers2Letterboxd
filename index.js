const express = require('express')
const bodyParser = require("body-parser")
const cors = require("cors")

require('dotenv').config()

const app = express()
const port = process.env.PORT || "3300"

app.use(cors({origin: "http://localhost:3300"}))
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

app.use(require('./api/routes/index'))

app.listen(port, () => {
    console.log(`Listening to requests on http://localhost:${port}`)
})
