import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => {
  return (
    <header className="header">
      <div className="header-container">
        <div className="header-left">
          <Link to="/" className="header-logo">
            <img src="https://nguyenanntu.com/wp-content/uploads/2023/03/logo-nguyenanhtu.png" alt="Nguyen An Tu" />
          </Link>
          <div className="header-contact">
            <p>+84-902880088</p>
            <p>info@nguyenanhtu.com</p>
          </div>
        </div>
        <div className="header-center">
          <Link to="/" className="header-address">
            20 Tăng Nhơn Phú
          </Link>
        </div>
        <div className="header-right">
          <div className="header-currency">
            <p>VND</p>
          </div>
          <div className="header-search">
            <input
              type="text"
              placeholder="Tìm kiếm tại đây"
              className="header-search-input"
            />
            <button className="header-search-button">
              <i className="fa fa-search"></i>
            </button>
          </div>
          <div className="header-cart">
            <Link to="/cart" className="header-cart-link">
              <i className="fa fa-shopping-cart"></i>
              <span className="header-cart-count">5</span>
            </Link>
          </div>
          <div className="header-favorite">
            <Link to="/favorite" className="header-favorite-link">
              <i className="fa fa-heart"></i>
            </Link>
          </div>
        </div>
      </div>
    </header>
  );
};

export default Header;
