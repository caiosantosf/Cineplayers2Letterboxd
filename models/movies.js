module.exports = (sequelize, Sequelize) => {
  const Movie = sequelize.define("movies", {
    title: {
      type: Sequelize.STRING
    },
    year: {
      type: Sequelize.INTEGER
    }
  });

  return Movie;
};